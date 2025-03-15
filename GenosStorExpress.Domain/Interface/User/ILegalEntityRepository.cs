﻿using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Repository.Base;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Domain.Interface.User {
    public interface ILegalEntityRepository:
        ISupportsCreate<LegalEntity>,
        ISupportsGetByStringId<LegalEntity>,
        ISupportsList<LegalEntity>,
        ISupportsUpdate<LegalEntity>,
        ISupportsDeleteByStringId  {
    }
}