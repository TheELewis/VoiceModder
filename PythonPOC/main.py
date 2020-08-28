import wave
import _thread
import pyaudio
import sys
import struct
import numpy as np
from librosa import effects
from pitcher import pitchshift
#import sounddevice

CHUNK = 8
FORMAT = pyaudio.paInt16
CHANNELS = 2
RATE = 48000
RECORD_SECONDS = 2
WAVE_OUTPUT_FILENAME = "outputs/output.wav"

# def input_thread(a_list):
#     input()
#     a_list.append(True)





def main():
    p = pyaudio.PyAudio()

    silence = chr(0)*CHUNK*CHANNELS*2
    stream = p.open(format=FORMAT,
                    channels=CHANNELS,
                    rate=RATE,
                    input=True,
                    output=True,
                    frames_per_buffer=CHUNK)

    print("[INFO]:Recording from device...")

    frames = []
    swidth = 2


    for i in range(0, int(RATE / CHUNK * RECORD_SECONDS)):
        #print("[INFO]Recording sample {}".format(i))
        data = stream.read(CHUNK, exception_on_overflow=False)
        if data == '':
            data = silence

        fx_data = np.array(map(float, data))
        
        # #adding FX
        # data = np.array(wave.struct.unpack("%dh"%(len(data)/swidth), data))*2
        # data = np.fft.rfft(data)
        # data = effects.pitch_shift(data, RATE, n_steps=-5)
        # data = np.fft.irfft(data)
        # dataout = np.array(data*0.5, dtype='int16')
        # chunkout = wave.struct.pack("%dh"%(len(dataout)), *list(dataout))
        # stream.write(chunkout, CHUNK)
        # frames.append(chunkout)

        stream.write(data, CHUNK)
        frames.append(data)



    print("[INFO]:Done recording...")

    stream.stop_stream()
    stream.close()
    p.terminate()

    wf = wave.open(WAVE_OUTPUT_FILENAME, 'wb')
    wf.setnchannels(CHANNELS)
    wf.setsampwidth(p.get_sample_size(FORMAT))
    wf.setframerate(RATE)
    wf.writeframes(b''.join(frames))
    wf.close()




if __name__ == "__main__":
    main()