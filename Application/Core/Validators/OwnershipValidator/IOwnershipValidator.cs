using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Validators.OwnershipValidator
{
    public interface IOwnershipValidator
    {
        public Task ValidateAccountOwnership(Guid userId, Guid accountId);
        public Task ValidateResearchOwnership(Guid userId, Guid researchId);
        public Task ValidateLabTestOwnership(Guid userId, Guid labTestId);
        public Task ValidatePatientLabTest(Guid userId, Guid labTestId);
        public Task ValidatePatientLabTestResult(Guid userId, Guid labTestResultId);
        public Task ValidateLabTestResultOwnership(Guid userId, Guid labTestResultId);
        public Task ValidateUserEmailOwnership(Guid userId, string email);
    }
}
