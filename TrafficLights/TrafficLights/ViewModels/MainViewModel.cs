using ReactiveUI;
using System.Reactive;

namespace TrafficLights.ViewModels;

public class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Свойства элементов интерфейса
    /// </summary>
    #region Bound properties

    #region Red light state

    private bool _isRedLightOn;

    public bool IsRedLigthOn
    {
        get => _isRedLightOn;
        set => this.RaiseAndSetIfChanged(ref _isRedLightOn, value);
    }

    #endregion

    /// <summary>
    /// Состояное жёлтого огня
    /// </summary>
    #region Yellow light state

    private bool _isYellowLightOn;

    public bool IsYellowLigthOn
    {
        get => _isYellowLightOn;
        set => this.RaiseAndSetIfChanged(ref _isYellowLightOn, value);
    }

    #endregion

    #region Green light state

    private bool _isGreenLightOn;

    public bool IsGreenLigthOn
    {
        get => _isGreenLightOn;
        set => this.RaiseAndSetIfChanged(ref _isGreenLightOn, value);
    }

    #endregion

    #endregion

    #region Commands

    /// <summary>
    /// Команда для зажигания красного
    /// </summary>
    public ReactiveCommand<Unit, Unit> TurnRedLightOnCommand { get; }

    /// <summary>
    /// Команда для погашения красного
    /// </summary>
    public ReactiveCommand<Unit, Unit> TurnRedLightOffCommand { get; }


    public ReactiveCommand<Unit, Unit> TurnYellowLightOnCommand { get; }

    public ReactiveCommand<Unit, Unit> TurnYellowLightOffCommand { get; }


    public ReactiveCommand<Unit, Unit> TurnGreenLightOnCommand { get; }


    public ReactiveCommand<Unit, Unit> TurnGreenLightOffCommand { get; }


    #endregion


    public MainViewModel()
    {
        #region Commands to methods binding

        TurnRedLightOnCommand = ReactiveCommand.Create(TurnRedLightOn);
        TurnRedLightOffCommand = ReactiveCommand.Create(TurnRedLightOff);
        TurnYellowLightOnCommand = ReactiveCommand.Create(TurnYellowLightOn);
        TurnYellowLightOffCommand = ReactiveCommand.Create(TurnYellowLightOff);
        TurnGreenLightOnCommand = ReactiveCommand.Create(TurnGreenLightOn);
        TurnGreenLightOffCommand = ReactiveCommand.Create(TurnGreenLightOff);

        #endregion
    }



    #region Методы изменения состояния огней

    private void TurnRedLightOn()
    {
        IsRedLigthOn = true;
    }

    private void TurnRedLightOff()
    {
        IsRedLigthOn = false;
    }

    private void TurnYellowLightOn()
    {
        IsYellowLigthOn = true;
    }

    private void TurnYellowLightOff()
    {
        IsYellowLigthOn = false;
    }

    private void TurnGreenLightOn()
    {
        IsGreenLigthOn = true;
    }

    private void TurnGreenLightOff()
    {
        IsGreenLigthOn = false;
    }

    #endregion

}
