using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendedMethods {
    public static void SetVel(this Rigidbody2D rb, float? x = null, float? y = null) {
        var vel = rb.velocity;
        if (x is not null) { vel.x = x.Value; }
        if (y is not null) { vel.y = y.Value; }
        rb.velocity = vel;
    }
}
