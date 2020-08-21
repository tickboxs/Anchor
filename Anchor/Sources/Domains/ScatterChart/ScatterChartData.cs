
using System.Collections.Generic;

namespace Anchor.Domains.ScatterChart
{
    public class ScatterChartData
    {
        public ScatterChartData() { }

        public ScatterChartData(IList<ScatterChartDataset> datasets, string[] labels)
        {
            Datasets = datasets;
            Labels = labels;
        }

        public IList<ScatterChartDataset> Datasets { private set; get; }

        public string[] Labels { set; get; }
    }
}
