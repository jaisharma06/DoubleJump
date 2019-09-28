
using System;
public class EventManager
{

    public static Action OnGameOver;
    public static Action OnGameStart;
    public static Action<Blade> OnEnemyActive;
    public static Action<Blade> OnEnemyDeactive;
	public static Action OnNoEnemyLeft;
    public static Action OnInvertColor;
    public static Action OnJump;
    public static Action OnDoubleJump;

    public static void CallOnGameOver()
    {
        if (OnGameOver != null)
        {
            OnGameOver();
        }
    }

    public static void CallOnInvertColor()
    {
        if (OnInvertColor != null)
        {
            OnInvertColor();
        }
    }

    public static void CallOnGameStart()
    {
        if (OnGameStart != null)
        {
            OnGameStart();
        }
    }

    public static void CallOnEnemyActive(Blade enemy)
    {
        if (OnEnemyActive != null)
        {
            OnEnemyActive(enemy);
        }
    }

    public static void CallOnEnemyDeactive(Blade enemy)
    {
        if (OnEnemyDeactive != null)
        {
            OnEnemyDeactive(enemy);
        }
    }
	public static void CallOnNoEnemyLeft()
    {
        if (OnNoEnemyLeft != null)
        {
            OnNoEnemyLeft();
        }
    }

    public static void CallOnJump()
    {
        if (OnJump != null)
        {
            OnJump();
        }
    }

    public static void CallOnDoubleJump()
    {
        if (OnDoubleJump != null)
        {
            OnDoubleJump();
        }
    }
}
