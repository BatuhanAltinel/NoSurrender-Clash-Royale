using System;

public class EventManager
{
    public static Action<GameState> OnGameStateChanged;
    public static Action<Unit> OnUnitAttack;
    public static Action<CharacterUnit, CharacterState> OnCharacterStateChange;
}
