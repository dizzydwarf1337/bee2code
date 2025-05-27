using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Queries.LabTestingQueries;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Interfaces.Queries.UserQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Validators.OwnershipValidator
{
    public class OwnershipValidator : IOwnershipValidator
    {
        private readonly ILabTestQueryRepository _labTestQueryRepository;
        private readonly ILabTestResultQueryRepository _labTestResultQueryRepository;
        private readonly IResearchQueryRepository _researchQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository; 

        public OwnershipValidator(
            ILabTestQueryRepository labTestQueryRepository,
            ILabTestResultQueryRepository labTestResultQueryRepository,
            IResearchQueryRepository researchQueryRepository,
            IUserQueryRepository userQueryRepository)
        {
            _labTestQueryRepository = labTestQueryRepository;
            _labTestResultQueryRepository= labTestResultQueryRepository;
            _researchQueryRepository = researchQueryRepository;
            _userQueryRepository = userQueryRepository;
        }

        public Task ValidateAccountOwnership(Guid userId, Guid accountId)
        {
            return userId == accountId ? Task.CompletedTask : throw new AccessForbiddenException("ValidateAccountOwnership", userId.ToString(), "User id and Account id doesn't match");
        }

        public async Task ValidateLabTestOwnership(Guid userId, Guid labTestId)
        {
            var labTest = await _labTestQueryRepository.GetLabTestByIdAsync(labTestId,null,"Admin") ?? throw new AccessForbiddenException("ValidateLabTestOwnership", userId.ToString(),"User has no access to this test or test doesn't exists");
            if(labTest.CreatorId != userId) throw new AccessForbiddenException("ValidateLabTestOwnership", userId.ToString(), "User have no right for this resourse");
        }
        public async Task ValidatePatientLabTest(Guid userId,Guid labTestId)
        {
            var labTest = await _labTestQueryRepository.GetLabTestByIdAsync(labTestId, userId, "Patient") ?? throw new AccessForbiddenException("ValidatePatientLabTest", userId.ToString(), "User has no access to this test or test doesn't exists");
            if (labTest.PatientId != userId) throw new AccessForbiddenException("ValidatePatientLabTest", userId.ToString(), "User have no right for this resourse");
        }
        public async Task ValidateLabTestResultOwnership(Guid userId, Guid labTestResultId)
        {
            var labTestResult = await _labTestResultQueryRepository.GetLabTestResultByIdAsync(labTestResultId) ?? throw new AccessForbiddenException("ValidateLabTestResultOwnership",userId.ToString(),"User has no access to this test result or it doesn't exists");
            await ValidateLabTestOwnership(userId, labTestResult.LabTestId);
        }

        public async Task ValidateResearchOwnership(Guid userId, Guid researchId)
        {
            var research = await _researchQueryRepository.GetResearchByIdAsync(researchId,null,"Admin") ?? throw new AccessForbiddenException("ValidateResearchOwnership",userId.ToString(),"User has no access to this research or research doesn't exists");
            if (research.OwnerId != userId) throw new AccessForbiddenException("ValidateResearchOwnership", userId.ToString(), "User has no access to this resource");
        }

        public async Task ValidateUserEmailOwnership(Guid userId, string email)
        {
            var user = await _userQueryRepository.GetUserByIdAsync(userId);
            if (!user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) throw new AccessForbiddenException("ValidateUserEmailOwnership", userId.ToString(), "User email confirmation failed"); 

        }

        public async Task ValidatePatientLabTestResult(Guid userId, Guid labTestResultId)
        {
            var labTestResult = await _labTestResultQueryRepository.GetLabTestResultByIdAsync(labTestResultId) ?? throw new AccessForbiddenException("ValidatePatientLabTestResult", userId.ToString(), "User has no access to this test result or it doesn't exists");
            var labTest = await _labTestQueryRepository.GetLabTestByIdAsync(labTestResult.LabTestId, null, "Admin");
            if (labTest.PatientId != userId)
            {
                throw new AccessForbiddenException("ValidatePatientLabTestResult", userId.ToString(), "User has no access to this resource");
            }
        }
    }
}
