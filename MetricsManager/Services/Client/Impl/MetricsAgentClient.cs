using MetricsManager.Models;
using MetricsManager.Models.Requests.Cpu;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace MetricsManager.Services.Client.Impl
{
    public class MetricsAgentClient : IMetricsAgentClient
    {

        #region Services

        private readonly AgentPool _agentPool;
        private readonly HttpClient _httpClient;

        #endregion

        public MetricsAgentClient(HttpClient httpClient,
            AgentPool agentPool)
        {
            _httpClient = httpClient;
            _agentPool = agentPool;
        }

        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/cpu/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                CpuMetricsResponse cpuMetricsResponse =
                    (CpuMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(CpuMetricsResponse));
                cpuMetricsResponse.AgentId = request.AgentId;
                return cpuMetricsResponse;
            }
            return null;
        }

        public DotnetMetricsResponse GetDotnetMetrics(DotnetMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/dotnet/errors - count/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                DotnetMetricsResponse dotnetMetricsResponse =
                    (DotnetMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(DotnetMetricsResponse));
                dotnetMetricsResponse.AgentId = request.AgentId;
                return dotnetMetricsResponse;
            }
            return null;
        }

        public HddMetricsResponse GetHddMetrics(HddMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/hdd/left/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                HddMetricsResponse hddMetricsResponse =
                    (HddMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(HddMetricsResponse));
                hddMetricsResponse.AgentId = request.AgentId;
                return hddMetricsResponse;
            }
            return null;
        }

        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/network/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                NetworkMetricsResponse networkMetricsResponse =
                    (NetworkMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(NetworkMetricsResponse));
                networkMetricsResponse.AgentId = request.AgentId;
                return networkMetricsResponse;
            }
            return null;
        }

        public RamMetricsResponse GetRamMetrics(RamMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/ram/available/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                RamMetricsResponse ramMetricsResponse =
                    (RamMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(RamMetricsResponse));
                ramMetricsResponse.AgentId = request.AgentId;
                return ramMetricsResponse;
            }
            return null;
        }
    }
}
