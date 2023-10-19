using System.Collections;

using System.Collections.Generic;
using _Scripts.Player;
using _Scripts.Player.Dice;
using _Scripts.Player.Pawn;
using Shun_Card_System;
using Shun_Unity_Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HandDice : PlayerEntity, ITargeter<HandDice>
{

    private PlayerDiceHand _playerDiceHand;
    [SerializeField, ShowImmutable] DiceDescription _diceDescription;
    private ITargeter<HandDice> _targeterImplementation;
 
    
    public void Initialize(PlayerDiceHand playerDiceHand, int containerIndex, ulong ownerClientID , DiceDescription diceDescription)
    {
        _playerDiceHand = playerDiceHand;
        _diceDescription = diceDescription;
        Initialize(containerIndex, ownerClientID);
    }

    protected virtual int GetNumber()
    {
        return Random.Range(_diceDescription.DiceLowerRange, _diceDescription.DiceUpperRange);
    }
    

    public virtual void ExecuteTargeter<TTargetee>(TTargetee targetee) where TTargetee : PlayerEntity
    {
        if (targetee is PlayerPawn playerPawn)
        {
            
            playerPawn.Move(GetNumber());
            _playerDiceHand.PlayDice(this);
        
            // Inherit this class and write Dice effect
            Debug.Log(name + " Dice drag to Pawn " + playerPawn.name);
            return;
        }
        else if (targetee is DiceCardConverter)
        {
            Debug.Log("Draw a card");
        }
        
        Destroy();
    }
}
