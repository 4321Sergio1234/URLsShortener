import {createBrowserRouter, RouterProvider} from 'react-router-dom';
import RootLayout from './pages/RootLayout';
import ShortUrlTable, {shortUrlLoader} from './pages/ShortUrlTable';
import ShortUrlInfo, {shortUrlInfoLoader, shortUrlDeleteAction} from './pages/ShortUrlInfo';
import About from './pages/About';
import Login, {authAction} from './pages/Login';
import { staticContentLoader, updateStaticContextAction } from './components/StaticContent.jsx';
import Error from './pages/Error.jsx';
import { logout } from './pages/Logout.jsx';
import { readUserData } from './utils/localStorageUtil.js';
import ShortUrlNew, { createShortUrlAction } from './pages/ShortUrlNew.jsx';
const router = createBrowserRouter([
  { 
    index: '/', 
    element: <RootLayout/>,
    errorElement: <Error/>,
    loader: readUserData,
    id: 'root',
    children: [
      { index: true, element: <ShortUrlTable/>, loader: shortUrlLoader},
      { path: ':id', element: <ShortUrlInfo/>, loader: shortUrlInfoLoader},
      { path: 'new', element: <ShortUrlNew/>, action: createShortUrlAction},
      { path: 'about', element: <About/>, loader: async ()=> staticContentLoader('about-view'),
                                          action: async (con)=> updateStaticContextAction({...con, pageTag:"about-view"})
      },
      { path: 'login', element: <Login/>, action: authAction }
    ]
  },
  { path: '/logout', action: logout },
  { path: '/short-url-delete/:id', action: shortUrlDeleteAction}
]);

function App() {

  return (
      <RouterProvider router={router}></RouterProvider>
  )
}

export default App
