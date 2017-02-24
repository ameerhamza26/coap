using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public enum Permissions
    {
        CIF,
        AC,
        USER
    }

    public enum Rights
    {
        Create,
        Update,
        Read,
        Delete
    }

    public enum Roles
    {
        BRANCH_OPERATOR,
        COMPLIANCE_OFFICER,
        BRANCH_MANAGER,
        IT_OPERATIONS
    }

    public enum UserType
    {
        Branch,
        Region,
        ITOPERATION
    }

    public enum AccountOpenTypes
    {
        INDIVIDUAL,
        BUSINESS,
        GOVERNMENT,
        OFFICE
    }

    public enum CifType
    {
        INDIVIDUAL,
        NEXT_OF_KIN,
        BUSINESS,
        GOVERNMENT,
        OFFICE,
        MINOR
    }

    public enum Status
    {
        SAVED,
        SUBMITTED,
        REJECTEBY_COMPLIANCE_MANAGER,
        SUBMITTED_BY_BRANCH_OPERATOR,
        UPDATED_CIF_APPROVED_BY_COMPAINCE_OFFICER,
        UPDATED_CIF_REJECTED_BY_COMPAINCE_OFFICER,
        UPDATED_CIF_REJECTED_BY_BRANCH_MANAGER,
        UPDATED_CIF_APPROVED_BY_BRANCH_MANAGER,
        APPROVED_BY_COMPLIANCE_MANAGER,
        REJECTED_BY_BRANCH_MANAGER,
        APPROVED_BY_BRANCH_MANAGER,
        UPDATED_BY_BRANCH_OPERATOR,
        PROFILE
    }


}
