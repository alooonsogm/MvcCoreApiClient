using MvcCoreApiClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcCoreApiClient.Services
{
    public class ServiceHospital
    {
        private string ApiUrl;
        //Necesitamos indicar el tipo de datos que vamos a leer.
        private MediaTypeWithQualityHeaderValue header;

        public ServiceHospital(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiHospitales");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            //Se utiliza la clase httpClient para las peticiones
            using (HttpClient client = new HttpClient())
            {
                string request = "api/hospitales";
                //Indicamos el host
                client.BaseAddress = new Uri(this.ApiUrl);
                //Indicamos los datos que vamos a consumir, limpiamos las cabeceras por noma.
                client.DefaultRequestHeaders.Clear();
                //Indicamos el tipo de datos que vamos a leer.
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //Realizamos la peteción y capturamos la respuesta.
                HttpResponseMessage response = await client.GetAsync(request);
                //En la respuesta tenemos la clave si deseamos personalizar errores.
                if (response.IsSuccessStatusCode == true)
                {
                    //Si la respuesta es correcta, leemos los datos en json.
                    string json = await response.Content.ReadAsStringAsync();
                    //mediante newton serializamos el json a List.
                    return JsonConvert.DeserializeObject<List<Hospital>>(json);
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Hospital> FindHospitalAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/hospitales/" + id;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode == true)
                {
                    //Si las propiedades del model y del json, se llama igual, no es necesario decorar con JsonProperty y tampoco usar JsonConvert.
                    return await response.Content.ReadAsAsync<Hospital>();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
