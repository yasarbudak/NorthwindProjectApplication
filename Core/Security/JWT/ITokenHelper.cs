﻿using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT
{
  public interface ITokenHelper
  {
    AccessToken CreatToken(User user, List<OperationClaim> operationClaims);
  }
}
