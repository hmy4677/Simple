

/**
 * 播放提示音
 * @param audioId 音频id
 */
export const soundsPlay = async (audioId: string) => {
    try {
        const audio: any = document.getElementById(audioId);
        audio.play();
    }
    catch (error) {
        console.error(error);
    }
}

//用例
// import { DeerPhotoOrderMP3, OrderFailTooMuchMP3, OrderNoResultMP3, OrderOutTimeMP3 } from '@assets/sounds';
// <audio id='audio_deerphoto' > <source src={ DeerPhotoOrderMP3 } /></audio >
//     <audio id='audio_noresult' > <source src={ OrderNoResultMP3 } /></audio >
//         <audio id='audio_outtime' > <source src={ OrderOutTimeMP3 } /></audio >
//             <audio id='audio_failtoomuch' > <source src={ OrderFailTooMuchMP3 } /></audio >