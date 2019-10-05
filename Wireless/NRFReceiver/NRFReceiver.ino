#include <SPI.h>
#include "nRF24L01.h"
#include "RF24.h"

RF24 radio(9, 10);
struct Payload
{
    uint8_t type;
    float x;
    float y;
};

Payload PLoad;
const uint64_t pipes[2] = {0xF0F0F0F0E1LL, 0xF0F0F0F0D2LL};

void setup(void)
{
    Serial.begin(57600);
    initReceiverRadio();
}

void loop(void)
{

    if (radio.available())
    {
        radio.read(&PLoad, sizeof(Payload));
        Serial.print(PLoad.type);
        Serial.print(' ');
        Serial.print(PLoad.x);
        Serial.print(' ');
        Serial.println(PLoad.y);
        delay(20);
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