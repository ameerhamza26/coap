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
        GOVERNMENT
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
        APPROVED_BY_COMPLIANCE_MANAGER,
        REJECTED_BY_BRANCH_MANAGER,
        APPROVED_BY_BRANCH_MANAGER,
        PROFILE
    }


}
