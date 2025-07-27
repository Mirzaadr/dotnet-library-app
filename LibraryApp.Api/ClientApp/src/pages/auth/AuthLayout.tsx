import { Outlet } from "react-router-dom";


const AuthLayout = () => {
  return (
    <main className="auth-container">
      <section className="auth-form">
        <div className='auth-box'>
          <div className="flex flex-row gap-3">
            <img src="https://book-smart-zeta.vercel.app/icons/logo.svg" alt="logo" width={37} height={37} />
            <h1 className="text-2xl font-semibold text-white">BookWise</h1>
          </div>
          <div>
            <Outlet />
          </div>
        </div>
      </section>

      <section className="auth-illustration">
        <img src="https://book-smart-zeta.vercel.app/_next/image?url=%2Fimages%2Fauth-illustration.png&w=1080&q=75" alt="auth-illustration" width={1000} height={1000} className="size-full object-cover"/>
      </section>
    </main>
  )

  // return (
  //   <div className="flex items-center justify-center min-h-screen bg-gray-100">
  //     <div className="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
  //       <h2 className="text-2xl font-semibold mb-6 text-center">Auth</h2>
  //       <Outlet />
  //     </div>
  //   </div>
  // );
}

export default AuthLayout;