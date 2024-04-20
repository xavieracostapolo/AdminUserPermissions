import { NavLink, Outlet } from 'react-router-dom'

type LinkActive = { isActive: boolean }

const isActiveLink = ({ isActive }: LinkActive) =>
    `link ${isActive ? 'active' : ''}`

export const Layout = () => {
    return (
        <>
            <nav>
                <NavLink className={isActiveLink} to="/">
                    Home
                </NavLink>
                <NavLink className={isActiveLink} to="/home2">
                    Home 2
                </NavLink>
            </nav>

            <div className="container">
                <Outlet />
            </div>
        </>
    )
}
