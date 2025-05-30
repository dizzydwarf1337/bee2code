﻿using Application.DTO.LabTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.LabTesting
{
    public interface ILabTestResultService
    {
        Task<LabTestResultDto> CreateLabTestResultAsync(CreateLabTestResultDto labTestResult);
        Task DeleteLabTestResultAsync(Guid labTestResultId);
        Task<LabTestResultDto> UpdateLabTestResultAsync(EditLabTestResultDto labTestResult);
        Task<LabTestResultDto> GetLabTestResultByIdAsync(Guid labTestResultId);
    }
}
