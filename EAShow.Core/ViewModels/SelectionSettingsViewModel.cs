using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using EAShow.Shared.Abstractions.Interfaces;
using EAShow.Shared.Events;
using EAShow.Shared.Models;
using EAShow.Core.Helpers;
using Nito.Mvvm;

namespace EAShow.Core.ViewModels
{
    public class SelectionSettingsViewModel : Screen, IPreset
    {
        private short _enabledCount;
        private bool _isSelection1Included;
        private bool _isSelection2Included;
        private List<Selections> _SelectionInts;
        private Selections _Selection1;
        private Selections _Selection2;

        private readonly IEventAggregator _eventAggregator;

        public short EnabledCount
        {
            get => _enabledCount;
            private set
            {
                Set(oldValue: ref _enabledCount, newValue: value, nameof(EnabledCount));
                NotifyTask.Create(asyncAction: PublishEnabledCountAsync);
            }
        }

        public bool IsSelection1Included
        {
            get => _isSelection1Included;
            set
            {
                Set(oldValue: ref _isSelection1Included, newValue: value, propertyName: nameof(IsSelection1Included));
                RefreshEnabledCount();
            }
        }

        public bool IsSelection2Included
        {
            get => _isSelection2Included;
            set
            {
                Set(oldValue: ref _isSelection2Included, newValue: value, propertyName: nameof(IsSelection2Included));
                RefreshEnabledCount();
            }
        }

        public Selections Selection1
        {
            get => _Selection1;
            set => Set(oldValue: ref _Selection1, newValue: value, propertyName: nameof(Selection1));
        }

        public Selections Selection2
        {
            get => _Selection2;
            set => Set(oldValue: ref _Selection2, newValue: value, propertyName: nameof(Selection2));
        }

        public List<Selections> SelectionInts
        {
            get => _SelectionInts;
            set => Set(oldValue: ref _SelectionInts, newValue: value, propertyName: nameof(SelectionInts));
        }

        public SelectionSettingsViewModel(IEventAggregator eventAggregator)
        {
            SelectionInts = new List<Selections>(collection: EnumHelper.GetValuesAsReadOnlyCollection<Selections>());
            _eventAggregator = eventAggregator;
        }
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await RestoreDefaultsAsync();
            await base.OnInitializeAsync(cancellationToken: cancellationToken);
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _eventAggregator.SubscribeOnUIThread(subscriber: this);
            return base.OnActivateAsync(cancellationToken);
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(subscriber: this);
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        // ReSharper disable once RedundantAwait
        // Do not remove async/await. This somehow causes HandleAsync<> not to fire.
        public Task PublishEnabledCountAsync()
        {
            return _eventAggregator.PublishOnBackgroundThreadAsync(message: new PresetEnabledCountChangedEvent
            {
                Preset = Presets.Selection,
                Count = EnabledCount
            });
        }

        public Task RestoreDefaultsAsync()
        {
            Selection1 = SelectionInts[0];
            Selection2 = SelectionInts[0];
            IsSelection1Included = false;
            IsSelection2Included = false;
            return Task.CompletedTask;
        }

        public Task HandleAsync(PresetResetRequestedEvent message, CancellationToken cancellationToken)
        {
            return Execute.OnUIThreadAsync(RestoreDefaultsAsync);
        }

        private void RefreshEnabledCount()
        {
            short count = default;
            if (IsSelection1Included)
                count++;
            if (IsSelection2Included)
                count++;

            EnabledCount = count;
        }
    }
}
