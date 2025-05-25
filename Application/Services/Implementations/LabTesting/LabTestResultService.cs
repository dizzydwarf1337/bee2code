using Application.DTO.LabTesting;
using Application.Services.Interfaces.LabTesting;
using AutoMapper;
using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.LabTestingCommands;
using Domain.Interfaces.Queries.LabTestingQueries;
using Domain.Models.LabTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.LabTesting
{
    public class LabTestResultService : ILabTestResultService
    {
        private readonly ILabTestResultCommandRepository _labTestResultCommandRepository;
        private readonly ILabTestResultQueryRepository _labTestResultQueryRepository;
        private readonly ILabTestQueryRepository _labTestQueryRepository;
        private readonly IMapper _mapper;
        public LabTestResultService(ILabTestResultCommandRepository labTestResultCommandRepository,
            ILabTestResultQueryRepository labTestResultQueryRepository,
            IMapper mapper,
            ILabTestQueryRepository labTestQueryRepository)
        {
            _labTestResultCommandRepository = labTestResultCommandRepository;
            _labTestResultQueryRepository = labTestResultQueryRepository;
            _mapper = mapper;
            _labTestQueryRepository = labTestQueryRepository;
        }

        public async Task<LabTestResultDto> CreateLabTestResultAsync(CreateLabTestResultDto labTestResultDto)
        {
            var labTest = await _labTestQueryRepository.GetLabTestByIdAsync(Guid.Parse(labTestResultDto.LabTestId));

            var labTestResult = new LabTestResult
            {
                Result = labTestResultDto.Result,
                LabTestId = labTest.Id
            };
            labTest.LabTestResultId = labTestResult.Id;
            labTest.LabTestResult = labTestResult;
            return _mapper.Map<LabTestResultDto>(await _labTestResultCommandRepository.CreateLabTestResultAsync(labTestResult));

        }

        public Task DeleteLabTestResultAsync(Guid labTestResultId)
        {
            throw new NotImplementedException();
        }

        public Task<LabTestResultDto> GetLabTestResultByIdAsync(Guid labTestResultId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<LabTestResultDto>> GetLabTestResultsByLabTestIdAsync(Guid labTestId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<LabTestResultDto>> GetLabTestResultsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<LabTestResultDto>> GetLabTestResultsPaginatedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<LabTestResultDto> UpdateLabTestResultAsync(EditLabTestResultDto labTestResult)
        {
            throw new NotImplementedException();
        }
    }
}
