using System.Diagnostics;
using Artemis.Core;
using RGB.NET.Core;

namespace Artemis.Plugins.ExtendedNodes.nodes
{
    [Node(
        "Weighted Average", 
        "Calculates the weighted average of the input value using the specified interval and weight", 
        "Extended", 
        InputType = typeof(Numeric), 
        OutputType = typeof(Numeric)
    )]
    public class WeightedAverageNode : Node
    {
        private long _lastUpdateTimestamp = 0;
        private Numeric? _average = null;

        public InputPin<Numeric> Value { get; }
        public InputPin<Numeric> Weight { get; }
        public InputPin<Numeric> Interval { get; }

        public OutputPin<Numeric> Average { get; }

        public WeightedAverageNode()
        {
            Value = CreateInputPin<Numeric>("Value");
            Weight = CreateInputPin<Numeric>("Weight");
            Interval = CreateInputPin<Numeric>("Interval");

            Average = CreateOutputPin<Numeric>("Average");
        }

        public override void Evaluate()
        {
            double nextUpdateIn = Interval.Value - TimerHelper.GetElapsedTime(_lastUpdateTimestamp);

            if (nextUpdateIn <= 0)
            {
                if (_average.HasValue)
                {
                    _average = _average.Value * (1.0 - Weight.Value) + Value.Value * Weight.Value;
                    Average.Value = _average.Value;
                } 
                else
                {
                    _average = Value.Value;
                    Average.Value = _average.Value;
                }

                _lastUpdateTimestamp = Stopwatch.GetTimestamp();
            }
        }
    }
}
