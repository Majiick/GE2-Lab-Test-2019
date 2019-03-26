using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState1 : State {
    Fighter fighter;

    public ShootingState1(Fighter fighter) {
        this.fighter = fighter;
    }

    public override void Enter() {
        Debug.Log("Entered shooting stae.");
    }

    public override void Exit() {


    }

    void FireShot() {
        Bullet.SpawnBullet(fighter.transform.position, fighter.targetBase, fighter);
    }

    public override void Think() {
        if (Time.time >= fighter.timeLastFiredShot + 0.6f) {
            if (fighter.tiberium >= 1) {
                FireShot();
                fighter.timeLastFiredShot = Time.time;
                fighter.tiberium -= 1.0f;
            } else {
                owner.ChangeState(new ArriveToBaseState(fighter));
            }
        }
    }
}

