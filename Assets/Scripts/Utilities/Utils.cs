using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

static class Utils
{ 

  public static float GetAngleFromVectorFloat(Vector3 dir)
  {
    dir = dir.normalized;
    float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    if (n < 0) n += 360;
    return n;
  }

  public static T RandomInRange<T>( T[] array )
  {
    return array[UnityEngine.Random.Range(0, array.Length)];
  }

  // public static void Accelerate( Vector2 velocity, Vector2 acceleration, Vector2 maxSpeed )
  // {
  //   float velocityX = velocity.x;
  //   if (acceleration.x > 0) {
  //     velocityX += MathF.Sign(velocity.x) * acceleration.x * Time.fixedDeltaTime;
  //     if (velocityX > maxVelocity.x) velocityX = maxVelocity.x;
  //   } 
  //   float velocityY = velocity.y;
  //   if (acceleration.y > 0) {
  //     velocityY += MathF.Sign(velocity.y) * acceleration.y * Time.fixedDeltaTime;
  //     if (velocityY > maxVelocity.y) velocityY = maxVelocity.y;
  //   } 
  //   velocity = new Vector2(velocityX, velocityY);
  // }

  // public static void AccelerateX( Vector2 velocity, float accelerationX, float maxVelocityX )
  // {
  //   Accelerate( velocity, new Vector2(accelerationX, 0.0f),  new Vector2(maxVelocityX, 0.0f) );
  //   float velocityX = velocity.x;
  //   if (accelerationX > 0) velocityX += MathF.Sign(velocity.x) * accelerationX * Time.fixedDeltaTime;
  //   float velocityY = velocity.y;
  //   if (accelerationY > 0) velocityY += MathF.Sign(velocity.y) * accelerationY * Time.fixedDeltaTime;
  //   velocity = new Vector2(velocityX, velocityY);
  // }

  // public static float CalculateVelocity( float currentVelocity, float acceleration  )
  // {
  //   return CalculateVelocity( currentVelocity, acceleration, Time.fixedDeltaTime);
  // }

};