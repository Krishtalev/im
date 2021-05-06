using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Imitation
{
    class Model
    {
        public double current_time;
        public int current_state;
        public int events;
        public double[] Statistic;
        private double width_border;
        private double max_border;
        public MarkovianChain randomEnvironment;
        public ArrivalProcess arrivalProcess;
        public Service service;

        public Model(double width, double max_value)
        {
            randomEnvironment = new MarkovianChain();
            arrivalProcess = new ArrivalProcess();
            service = new Service();

            current_state = randomEnvironment.get_state();
            current_time = 0;
            events = 0;
            width_border = width;
            max_border = max_value;
            Statistic = new double[(int)(max_border / width_border) + 1];

            randomEnvironment.nextState(current_time);
            arrivalProcess.calculateTime(current_time,current_state);
        }

        public void simulate(int max)
        {
            while (events < max)
            {
                events++;
                double ts = randomEnvironment.get_ts();
                double ta = arrivalProcess.get_ta();
                double tl = service.findNearest();
                double min_time = Math.Min(ts, Math.Min(ta, tl));

                double dt = min_time - current_time;
                double v = service.calculateVolume();
                if (v == 0)
                {
                    Statistic[0] += dt;
                }
                else
                {
                    int id = (int)(v / width_border) + 1;
                    Statistic[id] += dt;
                }

                current_time = min_time;
                if (min_time == ts)
                {
                    randomEnvironment.nextState(current_time);
                    current_state = randomEnvironment.get_state();
                    arrivalProcess.calculateTime(current_time, current_state);
                    service.on_state_shift(current_state, current_time);
                }
                if (min_time == ta)
                {
                    service.addRequest(current_state, current_time);
                    arrivalProcess.calculateTime(current_time, current_state);
                }
                if (min_time == tl) service.serveRequest();
            } 
            for (int i = 0; i < Statistic.Length; i++)
            {
                Statistic[i] /= current_time;
            }
            exportStatistic();
        }
        public void exportStatistic()
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workBook;
            Excel.Worksheet workSheet;
            workBook = excelApp.Workbooks.Add();
            workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            // Заполняем первый столбец листа из массива Y[0..n-1]
            for (int j = 1; j <= Statistic.Length; j++)
                workSheet.Cells[j, 1] = Statistic[j - 1];
            excelApp.Visible = true;
            excelApp.UserControl = true;
        }
        public int get_i()
        {
            return service.calculate_requests();
        }
        public double get_v()
        {
            return service.calculateVolume();
        }
    }
}
