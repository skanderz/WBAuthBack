﻿using AutoMapper;
using WBAuth.BLL.IManager;
using WBAuth.BO;
using WBAuth.DAL.IRepository;



namespace WBAuth.BLL.Manager
{
    public class ApplicationManager : IApplicationManager
    {
        private readonly IApplicationRepository _IApplicationRepository;
        private readonly IMapper _mapper;

        public ApplicationManager(IApplicationRepository IApplicationRepository, IMapper mapper)
        {
            _IApplicationRepository = IApplicationRepository;
            _mapper = mapper;
        }


        public async Task<int> Ajouter(Application oApplication)
        {
            if (oApplication == null) throw new ArgumentNullException(nameof(oApplication));
            var entity = _mapper.Map<DAL.Models.Application>(oApplication);
            var id = await _IApplicationRepository.Ajouter(entity);
            return id;
        }


        public async Task<IEnumerable<Application>> ChargerAll()
        {
            var Applications = await _IApplicationRepository.ChargerAll();
            var model = _mapper.Map<List<Application>>(Applications);
            return model;
        }


        public async Task<IEnumerable<Application>> Recherche(string str)
        {
            var Applications = await _IApplicationRepository.Recherche(str);
            var model = _mapper.Map<List<Application>>(Applications);
            return model;
        }

        public async Task<Application> RechercheById(int id)
        {
            var oApp = await _IApplicationRepository.RechercheById(id);
            var model = _mapper.Map<Application>(oApp);
            return model;
        }

        public async Task<int> Modifier(Application oApplication)
        {
            if (oApplication == null) throw new ArgumentNullException(nameof(oApplication));
            var entity = _mapper.Map<DAL.Models.Application>(oApplication);
            var id = await _IApplicationRepository.Modifier(entity);
            return id;
        }


        public async Task<bool> Supprimer(int Id) { return await _IApplicationRepository.Supprimer(Id); }



    }
}
