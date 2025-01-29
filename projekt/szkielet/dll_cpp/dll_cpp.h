// dll_cpp.h 
#pragma once

#ifdef DLL_CPP_EXPORTS
#define DLL_CPP_API __declspec(dllexport)
#else
#define DLL_CPP_API __declspec(dllimport)
#endif


extern "C" DLL_CPP_API int grayPixels(int wB, int wG, int wR, int wS,
    int arrayS, int increment, int startingIndex, uint8_t* table);