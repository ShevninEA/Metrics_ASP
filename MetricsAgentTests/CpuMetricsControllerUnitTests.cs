using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ICpuMetricsRepository> mock;

        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            _controller = new CpuMetricsController(mock.Object);
        }

        [Fact]
        public void GetCpuMetrics_ReturnOk()
        {
            mock.Setup(repository =>
                repository.Create(It.IsAny<CpuMetrics>())).Verifiable(); TimeSpan toTime = TimeSpan.FromSeconds(100);
            var result = _controller.Create(new MetricsAgent.Models.Requests.CpuMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetrics>()), Times.AtMostOnce());
        }
    }
}
