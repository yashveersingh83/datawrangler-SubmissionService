using AutoMapper;
using SharedKernel;
using SubmissionService.Application;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Infrastructure.Services
{
    public class MileStoneService : IMileStoneService
    {
        private readonly IRepository<MileStone> repository;
        private readonly IMapper mapper;

        public MileStoneService(IRepository<MileStone> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
           // mapper = AutoMapper.MapperConfiguration.CreateMapper();
        }

        public async Task<MileStoneDto> CreateAsync(MileStoneDto entity)
        {
            var item = mapper.Map<MileStone>(entity);
             await repository.CreateAsync(item);
            return mapper.Map<MileStoneDto>(item);
        }

        public async Task<IReadOnlyCollection<MileStoneDto>> GetAllAsync()
        {
            var mileStones = await repository.GetAllAsync();
            return mapper.Map<List<MileStoneDto>>(mileStones);
        }

        public async Task<IReadOnlyCollection<MileStoneDto>> GetAllAsync(Expression<Func<MileStone, bool>> filter)
        {
            var mileStones = await repository.GetAllAsync(filter);
            return mapper.Map<List<MileStoneDto>>(mileStones);
          
        }

        public async Task<MileStoneDto> GetAsync(Guid id)
        {
            var item = await repository.GetAsync(id);
            return mapper.Map<MileStoneDto>(item);
        }

        public async Task<MileStoneDto> GetAsync(Expression<Func<MileStone, bool>> filter)
        {

            var mileStones = await repository.GetAsync(filter);
            return mapper.Map<MileStoneDto>(mileStones);
        }

        public async Task<List<MileStoneDto>> GetMileStones()
        {
            var mileStones = await repository.GetAllAsync();
            return mapper.Map<List<MileStoneDto>>(mileStones);
        }
        public async Task UpdateAsync(MileStoneDto entity)
        {
            var item = mapper.Map<MileStone>(entity);
            await repository.UpdateAsync(item);
        }
        public  void RemoveAsync(Guid id)
        {
            repository.RemoveAsync(id);
        }
    }
}
