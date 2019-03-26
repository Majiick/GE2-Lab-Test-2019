using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivingToAttackPos : State {
    Fighter fighter;

    public ArrivingToAttackPos(Fighter fighter) {
        this.fighter = fighter;
    }

    public override void Enter() {
        Debug.Log("entered ArrivingToAttackPos state");
        owner.GetComponent<Arrive>().targetPosition = Vector3.Lerp(fighter.parentBase.transform.position, fighter.targetBase.transform.position, 0.8f);
    }

    public override void Exit() {


    }

    public override void Think() {
        if (Vector3.Distance(fighter.transform.position, owner.GetComponent<Arrive>().targetPosition) < 10f) {
            owner.ChangeState(new ShootingState1(fighter));
        }
    }
}

