using AutoMapper;
using Grpc.Core;
using ServerApp.Entities;
using ServerApp.Protos;
using ServerApp.Repositories;
using PersonelService = ServerApp.Protos.PersonelService;
namespace ServerApp.Services
{
    public class GrpcPersonelService : PersonelService.PersonelServiceBase
    {
        private readonly IPersonelService _personelService;
        private readonly IMapper _mapper;
        public GrpcPersonelService(IPersonelService personekService, IMapper mapper)
        {
            _personelService = personekService;
            _mapper = mapper;
        }
        public async override Task<Personels> GetPersonelList(Empty request, ServerCallContext context)
        {
            var personelData = await _personelService.GetListAsync();
            Personels response = new Personels();
            foreach (Personel pers in personelData)
            {
                response.Items.Add(_mapper.Map<PersonelDetail>(pers));
            }
            return response;
        }
        public async override Task<PersonelDetail> GetPersonel(GetPersonelDetailRequest request, ServerCallContext context)
        {
            var personel = await _personelService.GetByIdAsync(request.PersonelId);
            var personelDetail1 = _mapper.Map<PersonelDetail>(personel);
            return personelDetail1;
        }
        public async override Task<PersonelDetail> CreatePersonel(CreatePersonelDetailRequest request, ServerCallContext context)
        {
            var personel = _mapper.Map<Personel>(request.Personel);
            await _personelService.AddAsync(personel);
            var personelDetail2 = _mapper.Map<PersonelDetail>(personel);
            return personelDetail2;
        }
        public async override Task<PersonelDetail> UpdatePersonel(UpdatePersonelDetailRequest request, ServerCallContext context)
        {
            var personel = _mapper.Map<Personel>(request.Personel);
            await _personelService.UpdateAsync(personel);
            var personelDetail = _mapper.Map<PersonelDetail>(personel);
            return personelDetail;
        }
        public async override Task<DeletePersonelDetailResponse> DeletePersonel(DeletePersonelDetailRequest request, ServerCallContext context)
        {
            var isDeleted = await _personelService.DeleteAsync(request.PersonelId);
            var response = new DeletePersonelDetailResponse
            {
                IsDelete = isDeleted
            };
            return response;
        }
    }
}
