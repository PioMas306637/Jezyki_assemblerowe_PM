// dll_cpp.cpp : Defines the exported functions for the DLL.
#include "pch.h" // use stdafx.h in Visual Studio 2017 and earlier
#include <utility>
#include <limits.h>
#include "dll_cpp.h"

int grayPixels(int wB, int wG, int wR, int wS,
    int arrayS, int increment, int startingIndex, uint8_t* table)
{
    for (int i = startingIndex; i < startingIndex + (arrayS * increment); i = i + increment)
    {
        int multipliedB = table[i] * wB;
        int multipliedG = table[i+1] * wG;
        int multipliedR = table[i+2] * wR;
        int sum = multipliedB + multipliedG + multipliedR;
        int newVal = sum / wS;
        table[i] = newVal;
        table[i + 1] = newVal;
        table[i + 2] = newVal;
    }
    return 0;
}