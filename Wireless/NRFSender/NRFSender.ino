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

void setup()
{
    Serial.begin(57600);
    initTransmitterRadio();
}

void loop(void)
{
    radio.stopListening();
    PLoad.type = 1;
    PLoad.x = 90.32F;
    PLoad.y = 3.14F;
    radio.write(&PLoad, sizeof(Payload));

    radio.startListening();

    delay(60);
}

void initTransmitterRadio()
{
    radio.begin();
    radio.setRetries(15, 15);
    radio.openWritingPipe(pipes[0]);
    radio.openReadingPipe(1, pipes[1]);
    radio.stopListening();
}