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
        // Commands
        public async Task<LabTestResultDto> CreateLabTestResultAsync(CreateLabTestResultDto labTestResultDto)
        {
            var labTest = await _labTestQueryRepository.GetLabTestByIdAsync(Guid.Parse(labTestResultDto.LabTestId), null, "Admin");

            var labTestResult = new LabTestResult
            {
                Result = labTestResultDto.Result,
                LabTestId = labTest.Id
            };
            labTest.LabTestResultId = labTestResult.Id;
            labTest.LabTestResult = labTestResult;
            return _mapper.Map<LabTestResultDto>(await _labTestResultCommandRepository.CreateLabTestResultAsync(labTestResult));

        }
        public async Task<LabTestResultDto> UpdateLabTestResultAsync(EditLabTestResultDto labTestResultDto)
        {
            var labTestResult = await _labTestResultQueryRepository.GetLabTestResultByIdAsync(Guid.Parse(labTestResultDto.Id));
            _mapper.Map(labTestResultDto, labTestResult);
            return _mapper.Map<LabTestResultDto>(await _labTestResultCommandRepository.UpdateLabTestResultAsync(labTestResult));
        }

        public async Task DeleteLabTestResultAsync(Guid labTestResultId)
        {
            await _labTestResultCommandRepository.DeleteLabTestResultAsync(labTestResultId);
        }

        // Queries

        public async Task<LabTestResultDto> GetLabTestResultByIdAsync(Guid labTestResultId)
        {
            return _mapper.Map<LabTestResultDto>(await _labTestResultQueryRepository.GetLabTestResultByIdAsync(labTestResultId));
        }

    }
}
