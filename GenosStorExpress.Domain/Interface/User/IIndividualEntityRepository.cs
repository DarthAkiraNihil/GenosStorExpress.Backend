﻿using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Domain.Repository.Base;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Domain.Interface.User {
    public interface IIndividualEntityRepository :
        ISupportsCreate<IndividualEntity>,
        ISupportsGetByStringId< IndividualEntity>,
        ISupportsList<IndividualEntity>,
        ISupportsUpdate<IndividualEntity>,
        ISupportsDeleteByStringId {

    }
}