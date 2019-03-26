using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveToBaseState : State {
    Fighter fighter;

    public ArriveToBaseState(Fighter fighter) {
        this.fighter = fighter;
    }

    public override void Enter() {
        Debug.Log("Entered arriving to base state.");
        fighter.GetComponent<Arrive>().targetPosition = fighter.parentBase.transform.position;
    }

    public override void Exit() {


    }

    public override void Think() {
        if(Vector3.Distance(fighter.transform.position, fighter.parentBase.transform.position) <= 5f) {
            if (fighter.parentBase.GetComponent<Base>().TryRefuel()) {
                fighter.tiberium = 7;
                owner.ChangeState(new ArrivingToAttackPos(fighter));
            }
        }
    }
}

