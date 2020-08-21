
using System.Collections.Generic;

namespace Anchor.Domains.RadarChart
{
    public class RadarChartData
    {
        public RadarChartData(IList<RadarChartDataset> datasets, IList<string> labels)
        {
            Datasets = datasets;
            Labels = labels;
        }

        public IList<RadarChartDataset> Datasets { private set; get; }
        public IList<string> Labels { private set; get; }
    }
}
