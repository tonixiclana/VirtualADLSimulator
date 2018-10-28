/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Interface of Sensor Class
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISensor {

    /// <summary>
    /// Definition of notify event function, this function implement the way to send a signal 
    /// to RegistryActivityManager that catch the notifications
    /// </summary>
    /// <param name="eventNotification"></param>
    void notifyEvent(string eventNotification);

    /// <summary>
    /// Change the sensor value, only when detect changes, the velocity of execution of 
    /// setSensorValue depending of frecuency of sensor
    /// </summary>
    /// <param name="value"></param>
    bool setSensorValue(float value);
}
