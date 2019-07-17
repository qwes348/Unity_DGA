

// Timeline시 주의사항
// 1. Animation timeline track 들어간 객체는 Animator가 자동으로 생성이 된다. Animator Controller가 없어도 제가하면 안된다.

// 2. Animation 용으로 사용되는 Animator와 중복으로 사양할 수 없다. 하위 Model로 분리해야 한다.

// 3. 실시간 생성되는(Instantiate)객체는 실행시점에 Binding해야한다.

// 4. 실시간 생성되는 객체에대한 Timeline은 LocalTranform 기준으로 Animation 작업을 해야한다. World Zero로 옮겨서 작업해라

// 5. TrackOffset값을 Clear해야한다. track/clip 두개다있다 모두 Clear!
