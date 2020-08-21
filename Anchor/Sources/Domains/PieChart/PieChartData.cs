
using System.Collections.Generic;

namespace Anchor.Domains.PieChart
{
    public class PieChartData
    {
        public PieChartData() { }

        public PieChartData(IList<PieChartDataset> datasets)
        {
            Datasets = datasets;
        }

        public IList<PieChartDataset> Datasets { set; get; }
    }
}
