using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController _dotnetMetricsController;

        public DotNetMetricsControllerTests()
        {
            _dotnetMetricsController = new DotNetMetricsController();
        }

        [Fact]
        public void GetDotnetMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            var result = _dotnetMetricsController.GetDotnetMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
