import { Link } from 'react-router-dom'
import { useGetPermission } from '../application'

export const Home2 = () => {
    const { data, isLoading, isError } = useGetPermission()

    return (
        <>
            <h1>Home2</h1>

            {isLoading && <span>fetching a character...</span>}
            {isError && <span>Ups! it was an error 🚨</span>}

            <div className="grid">
                <ul>
                    {data?.map(item => (
                        <li key={item.nombreEmpleado}>{item.nombreEmpleado}</li>
                    ))}
                </ul>
            </div>

            <div className="grid">
                <Link to={`/`} className="user">
                    <span>username</span>
                </Link>
            </div>
        </>
    )
}
