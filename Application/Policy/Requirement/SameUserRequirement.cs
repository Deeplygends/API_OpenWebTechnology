using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Application.Policy.Requirement
{
    public class SameUserRequirement : IAuthorizationRequirement
    {
    }
}
