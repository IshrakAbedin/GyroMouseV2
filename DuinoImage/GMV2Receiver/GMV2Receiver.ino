#include <SPI.h>
#include "nRF24L01.h"
#include "RF24.h"

#define FILTER_RESOLUTION 5
#define X_ANGLE_MAX_DEGREE 25
#define Y_ANGLE_MAX_DEGREE 18

RF24 radio(9, 10);
struct Payload
{
    uint8_t type;
    float x;
    float y;
};

Payload PLoad;
const uint64_t pipes[2] = {0xF0F0F0F0E1LL, 0xF0F0F0F0D2LL};
float xbuffer[FILTER_RESOLUTION];
float ybuffer[FILTER_RESOLUTION];
uint8_t counter = 0;
int LMBPreviousState = 0;
int RMBPreviousState = 0;

void setup(void)
{
    for (uint8_t i = 0; i < FILTER_RESOLUTION; i++)
    {
        xbuffer[i] = 0;
        ybuffer[i] = 0;
    }
    Serial.begin(57600);
    initReceiverRadio();
}

void loop(void)
{

    if (radio.available())
    {
        radio.read(&PLoad, sizeof(Payload));
        if(PLoad.type == 1){
            runSmoothingAverageFilter();
        }
        else if(PLoad.type == 2){
            printMouseStateChange();
        }
        // Serial.print(PLoad.type);
        // Serial.print(' ');
        // Serial.print(PLoad.x);
        // Serial.print(' ');
        // Serial.println(PLoad.y);
        // delay(20);
    }
}

void initReceiverRadio()
{
    radio.begin();
    radio.setRetries(15, 15);
    radio.openWritingPipe(pipes[1]);
    radio.openReadingPipe(1, pipes[0]);
    radio.startListening();
}

void runSmoothingAverageFilter()
{
    xbuffer[counter] = PLoad.x;
    ybuffer[counter] = PLoad.y;
    counter = (counter + 1) % FILTER_RESOLUTION;
    if (counter == 0)
    {
        Serial.print("G ");
        Serial.print(getBoundedAvgX());
        Serial.print(' ');
        Serial.print(getBoundedAvgY());
        Serial.println();
    }
}

float getBoundedAvgX()
{
    float bax = 0.0f;
    for (uint8_t i = 0; i < FILTER_RESOLUTION; i++)
    {
        bax += xbuffer[i];
    }
    bax = bax / FILTER_RESOLUTION;
    if (bax < -1 * X_ANGLE_MAX_DEGREE)
    {
        bax = -1 * X_ANGLE_MAX_DEGREE;
    }
    else if (bax > X_ANGLE_MAX_DEGREE)
    {
        bax = X_ANGLE_MAX_DEGREE;
    }
    return bax;
}

float getBoundedAvgY()
{
    float bay = 0.0f;
    for (uint8_t i = 0; i < FILTER_RESOLUTION; i++)
    {
        bay += ybuffer[i];
    }
    bay = bay / FILTER_RESOLUTION;
    if (bay < -1 * Y_ANGLE_MAX_DEGREE)
    {
        bay = -1 * Y_ANGLE_MAX_DEGREE;
    }
    else if (bay > Y_ANGLE_MAX_DEGREE)
    {
        bay = Y_ANGLE_MAX_DEGREE;
    }
    return bay;
}

void printMouseStateChange(){
    if(int(PLoad.x) != LMBPreviousState){
        LMBPreviousState = int(PLoad.x);
        Serial.print("L ");
        Serial.print(LMBPreviousState);
        Serial.println();
    }
    if(int(PLoad.y) != RMBPreviousState){
        RMBPreviousState = int(PLoad.y);
        Serial.print("R ");
        Serial.print(RMBPreviousState);
        Serial.println();
    }
}
