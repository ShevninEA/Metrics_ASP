﻿//using MetricsManager.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MetricsManagerTests
//{
//    public class RamMetricsControllerTests
//    {
//        private RamMetricsController _ramMetricsController;

//        public RamMetricsControllerTests()
//        {
//            _ramMetricsController = new RamMetricsController();
//        }

//        [Fact]
//        public void GetMetricsFromAgent_ReturnOk()
//        {
//            // Подготовка данных для тестирования
//            int agentId = 1;
//            TimeSpan fromTime = TimeSpan.FromSeconds(0);
//            TimeSpan toTime = TimeSpan.FromSeconds(100);

//            // Исполнение тестируемого метода
//            var result = _ramMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

//            // Подготовка эталонного результата, проверка результата
//            Assert.IsAssignableFrom<IActionResult>(result);
//        }

//        [Fact]
//        public void GetMetricsFromAllCluster_ReturnOk()
//        {
//            TimeSpan fromTime = TimeSpan.FromSeconds(0);
//            TimeSpan toTime = TimeSpan.FromSeconds(100);
//            var result = _ramMetricsController.GetMetricsFromAll(fromTime, toTime);
//            Assert.IsAssignableFrom<IActionResult>(result);
//        }
//    }
//}
