using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artemis.Core.Nodes;
using Artemis.Plugins.ExtendedNodes.nodes;
using Serilog;

namespace Artemis.Plugins.ExtendedNodes
{
    public class ExendedNodesProvider : NodeProvider
    {
        private readonly ILogger _logger;

        public ExendedNodesProvider(ILogger logger)
        {
            _logger = logger;
        }

        public override void Enable()
        {
            try
            {
                RegisterNodeType<WeightedAverageNode>();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error enabling extended nodes provider");
            }
        }

        public override void Disable()
        {
        }
    }
}
