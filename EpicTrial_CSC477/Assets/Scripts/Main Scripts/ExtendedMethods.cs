using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendedMethods {

    /**Change the velocity of a Rigidbody2D**/
    public static void SetVel(this Rigidbody2D rb, float? x = null, float? y = null) {
        var vel = rb.velocity;
        if (x is not null) { vel.x = x.Value; }
        if (y is not null) { vel.y = y.Value; }
        rb.velocity = vel;
    }
    /**Change the local position of a transform.**/
    public static void SetLocalPos(this Transform tf, float? x = null, float? y = null) {
        var pos = tf.localPosition;
        if (x is not null) { pos.x = x.Value; }
        if (y is not null) { pos.y = y.Value; }
        tf.localPosition = pos;
    }
    /**Change the local scale of a transform.**/
    public static void SetLocalScale(this Transform tf, float? x = null, float? y = null) {
        var scale = tf.localScale;
        if (x is not null) { scale.x = x.Value; }
        if (y is not null) { scale.y = y.Value; }
        tf.localScale = scale;
    }
}
