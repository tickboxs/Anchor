
using System.Collections.Generic;

namespace Anchor.Domains.LineChart
{
    public class LineChartData
    {

        public LineChartData() { }

        public LineChartData(IList<LineChartDataset> datasets, string[] labels)
        {
            Datasets = datasets;
            Labels = labels;
        }

        public IList<LineChartDataset> Datasets { private set; get; }
        public string[] Labels { set; get; }
    }
}
