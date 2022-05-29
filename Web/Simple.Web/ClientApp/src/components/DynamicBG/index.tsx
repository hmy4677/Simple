import Particles from "react-tsparticles";
import { loadFull } from 'tsparticles';
import { IOptions } from "tsparticles-engine";
enum OutMode {
    bounce = "bounce",
    bounceHorizontal = "bounce-horizontal",
    bounceVertical = "bounce-vertical",
    none = "none",
    out = "out",
    destroy = "destroy",
    split = "split"
}
enum EasingType {
    easeOutBack = "ease-out-back",
    easeOutCirc = "ease-out-circ",
    easeOutCubic = "ease-out-cubic",
    easeOutQuad = "ease-out-quad",
    easeOutQuart = "ease-out-quart",
    easeOutQuint = "ease-out-quint",
    easeOutExpo = "ease-out-expo",
    easeOutSine = "ease-out-sine"
}
//背景配置
const options: IOptions = {
    autoPlay: true,
    background: {
        color: "#AED6BE",
        image: '',
        opacity: 100,
        position: "50% 50%",
        repeat: "no-repeat",
        size: "cover"
    },
    fpsLimit: 120,
    fullScreen: {
        zIndex: 1
    },
    detectRetina: true,
    interactivity: {
        events: {
            onClick: {
                enable: true,
                mode: "push"
            },
            onHover: {
                enable: true,
                mode: "slow",
                parallax: {
                    enable: true,
                    force: 10,
                    smooth: 10
                },
            },
            resize: true,
        },
        modes: {
            push: {
                quantity: 3,//点击是添加1个粒子
            },
            bubble: {
                distance: 200,
                duration: 2,
                opacity: 0.8,
                size: 20,
                divs: {
                    distance: 200,
                    duration: 0.4,
                    mix: false,
                    selectors: []
                }
            },
            grab: {
                distance: 400
            },
            //击退
            repulse: {
                divs: {
                    distance: 200,//鼠标移动时排斥粒子的距离
                    duration: 0.4,
                    factor: 100,
                    speed: 1,
                    maxSpeed: 50,
                    easing: EasingType.easeOutQuad,
                    selectors: []
                }
            },
            //缓慢移动
            slow: {
                //移动速度
                factor: 2,
                //影响范围
                radius: 200,
            },
            //吸引
            attract: {
                distance: 200,
                duration: 0.4,
                // "easing": "ease-out-quad",
                factor: 3,
                maxSpeed: 50,
                speed: 1

            },
        }
    },
    //  粒子的参数
    particles: {
        //粒子的颜色
        color: {
            value: "#ffffff"
        },
        //是否启动粒子碰撞
        collisions: {
            enable: true,
        },
        //粒子之间的线的参数
        links: {
            color: "#ffffff",
            distance: 150,
            enable: true,
            warp: true
        },
        move: {
            attract: {
                rotate: {
                    x: 600,
                    y: 1200
                }
            },
            enable: true,
            outModes: {
                bottom: OutMode.out,
                left: OutMode.out,
                right: OutMode.out,
                top: OutMode.out,
            },
            speed: 6,
            warp: true
        },
        number: {
            density: {
                enable: true
            },
            value: 40//初始粒子数
        },
        //透明度
        opacity: {
            value: 0.5,
            animation: {
                speed: 3,
                minimumValue: 0.1
            }
        },
        size: {
            random: {
                enable: true
            },
            value: {
                min: 1,
                max: 3
            },
            animation: {
                speed: 20,
                minimumValue: 0.1
            }
        }
    }
}

/**
 * 动态背景
 * @returns 
 */
const DynamicBG: React.FC = () => {
    const particlesInit = async (main: any) => {
        console.log(main);
        await loadFull(main);
    }
    return (
        <Particles
            id="tsparticles"
            init={particlesInit}
            options={options}
        />
    );
}

export default DynamicBG;