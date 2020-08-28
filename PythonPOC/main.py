import wave
import _thread
import pyaudio

CHUNK = 1024
FORMAT = pyaudio.paInt16
CHANNELS = 2
RATE = 44100
RECORD_SECONDS = 5
WAVE_OUTPUT_FILENAME = "..\\outputs\\ouput.wav"

# def input_thread(a_list):
#     input()
#     a_list.append(True)





def main():
    p = pyaudio.PyAudio()

    stream = p.open(format=FORMAT,
                    channels=CHANNELS,
                    rate=RATE,
                    input=True,
                    input_device_index=1,
                    output=True,
                    output_device_index=4,
                    frames_per_buffer=CHUNK)

    print("[INFO]:Recording from device...")

    frames = []
    a_list = []
    # _thread.start_new_thread(input_thread, (a_list,))

    # while not a_list:
    #     record_audio(stream, frames)
    
    for i in range(0, int(RATE / CHUNK * RECORD_SECONDS)):
        print("[INFO]Recording sample {}".format(i))
        data = stream.read(CHUNK)
        stream.write(data, CHUNK)
        frames.append(data)



    print("[INFO]:Done recording...")

    stream.stop_stream()
    stream.close()
    p.terminate()

    # wf = wave.open(WAVE_OUTPUT_FILENAME, 'wb')
    # wf.setnchannels(CHANNELS)
    # wf.setsampwidth(p.get_sample_size(FORMAT))
    # wf.setframerate(RATE)
    # wf.writeframes(b''.join(frames))
    # wf.close()




if __name__ == "__main__":
    main()