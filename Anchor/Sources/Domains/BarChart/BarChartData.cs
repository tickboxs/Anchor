
using System.Collections.Generic;

namespace Anchor.Domains.BarChart
{
    public class BarChartData
    {

        public BarChartData() { }

        public BarChartData(IList<BarChartDataset> datasets,string[] labels)
        {
            Datasets = datasets;
            Labels = labels;
        }

        public IList<BarChartDataset> Datasets { private set; get; }
        public string[] Labels { set; get; }
    }
}
