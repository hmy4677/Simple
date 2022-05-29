import styles from './index.less';
import { request } from 'umi';

export default function IndexPage() {

  const fetch = async () => {
    const res = request('/api/WeatherForecast')
    console.log(res);

  }

  return (
    <div>
      <h1 className={styles.title}>Page index</h1>
      <button onClick={fetch}>request</button>
    </div>
  );
}
 