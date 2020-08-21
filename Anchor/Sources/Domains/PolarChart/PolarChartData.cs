

namespace Anchor.Domains.PolarChart
{
    public class PolarChartData
    {
        public PolarChartData(PolarChartDataset dataset, string[] labels)
        {
            Dataset = dataset;
            Labels = labels;
        }

        public PolarChartDataset Dataset { private set; get; }
        public string[] Labels { private set; get; }
    }
}
