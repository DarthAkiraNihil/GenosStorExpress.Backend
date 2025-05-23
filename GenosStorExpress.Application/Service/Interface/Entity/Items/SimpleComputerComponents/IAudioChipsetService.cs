﻿using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Application.Wrappers.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Domain.Entity.Item.SimpleComputerComponent;
using GenosStorExpress.Utils.Operations;

namespace GenosStorExpress.Application.Service.Interface.Entity.Items.SimpleComputerComponents {
    /// <summary>
    /// Интерфейс сервиса аудиочипсетов
    /// </summary>
    public interface IAudioChipsetService :
        IStandardService<AudioChipsetWrapper>,
        ISupportsGetRaw<AudioChipset>;
}