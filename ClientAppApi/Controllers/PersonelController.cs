using Microsoft.AspNetCore.Mvc;
using ClientAppApi.Entities;
using Grpc.Net.Client;
using ServerApp.Protos;
using System;
using Empty = ServerApp.Protos.Empty;
using Google.Protobuf.WellKnownTypes;

namespace ClientAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly PersonelService.PersonelServiceClient _client;
        private readonly IConfiguration _configuration;
        public PersonelController(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel =GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:PersonelServiceUrl"));
            _client = new PersonelService.PersonelServiceClient(_channel);
        }
        [HttpGet("getpersonellist")]
        public async Task<Personels> GetPersonelListAsync()
        {
            try
            {
                var response = await _client.GetPersonelListAsync(new Empty { });
                return response;
            }
            catch
            {
            }
            return null;
        }
        [HttpGet("getpersonelbyid")]
        public async Task<PersonelDetail> GetByIdAsync(int Id)
        {
            try
            {
                var request = new GetPersonelDetailRequest
                {
                    PersonelId = Id
                };
                var response = await _client.GetPersonelAsync(request);
                return response;
            }
            catch
            {
            }
            return null;
        }
        [HttpPost("addpersonel")]
        public async Task<PersonelDetail> AddAsync(Personel pers)
        {
            try
            {
                var personelDetail = new PersonelDetail
                {
                    Id = pers.Id,
                    FirstName = pers.FirstName,
                    LastName = pers.LastName,
                    NationalCode = pers.NationalCode,
                    Birthday= Timestamp.FromDateTime(pers.Birthday)
                };
                var response = await _client.CreatePersonelAsync(new CreatePersonelDetailRequest()
                {
                    Personel = personelDetail
                });
                return response;
            }
            catch
            {
            }
            return null;
        }
        [HttpPut("updatepersonel")]
        public async Task<PersonelDetail> UpdateAsync(Personel pers)
        {
            try
            {
                var personelDetail = new PersonelDetail
                {
                    Id = pers.Id,
                    FirstName = pers.FirstName,
                    LastName = pers.LastName,
                    NationalCode= pers.NationalCode,
                    //Birthday= Timestamp.FromDateTime(pers.Birthday)

                };
                var response = await _client.UpdatePersonelAsync(new UpdatePersonelDetailRequest()
                {
                    Personel = personelDetail
                });
                return response;
            }
            catch
            {
            }
            return null;
        }
        [HttpDelete("deletepersonel")]
        public async Task<DeletePersonelDetailResponse> DeleteAsync(int Id)
        {
            try
            {
                var response = await _client.DeletePersonelAsync(new DeletePersonelDetailRequest()
                {
                    PersonelId = Id
                });
                return response;
            }
            catch
            {
            }
            return null;
        }
    }
}
